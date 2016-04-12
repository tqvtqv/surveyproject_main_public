using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.UserControls;
using System.Text.RegularExpressions;
using Votations.NSurvey.WebAdmin.Code;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Users = Votations.NSurvey.DataAccess.Users;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class UserImportExcel : System.Web.UI.UserControl
    {
        
        public bool ShowMessage { get { return ViewState["SHOWMESSAGE"] != null; } set { ViewState["SHOWMESSAGE"] = (value ? "X" : null); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();

            SetupSecurity();

            if (ShowMessage) ShowMessage = false;
            else MessageLabel.Visible = false;
            
            if (!Page.IsPostBack)
            {
            
                // Header.SurveyId = SurveyId;
                ((Wap)((PageBase)Page).Master).HeaderControl.SurveyId = ((PageBase)Page).getSurveyId();
                BindSurveyDropDownLists();
            }	
        }


        private void SetupSecurity()
        {
            // Check if we can edit extended properties
            if ((_userProvider is INSurveyUserProvider))
            {
                ((PageBase)Page).CheckRight(NSurveyRights.AccessUserManager, true);
            }
            else
            {
                UINavigator.NavigateToAccessDenied(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex);
            }
        }


        private void LocalizePage()
        {
            ImportUsersButton.Text = ((PageBase)Page).GetPageResource("ImportUsersButton");
            ImportUsersTitle.Text = ((PageBase)Page).GetPageResource("ImportUsersTitle");
            ImportUserLabel.Text = "Chọn file excel";
            
        }


        private void BindSurveyDropDownLists()
        {
            //SurveysListBox.DataSource = new Surveys().GetAllSurveysList();
            //SurveysListBox.DataMember = "Surveys";
            //SurveysListBox.DataTextField = "Title";
            //SurveysListBox.DataValueField = "SurveyId";
            //SurveysListBox.DataBind();

            //RolesListBox.DataSource = new Roles().GetAllRolesList();
            //RolesListBox.DataMember = "Roles";
            //RolesListBox.DataTextField = "RoleName";
            //RolesListBox.DataValueField = "RoleId";
            //RolesListBox.DataBind();

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImportUsersButton.Click += new System.EventHandler(this.ImportUsersButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private IUserProvider _userProvider = UserFactory.Create();
        private string ReadExcelCell(Cell cell, WorkbookPart workbookPart)
        {
            var cellValue = cell.CellValue;
            var text = (cellValue == null) ? cell.InnerText : cellValue.Text;
            if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
            {
                text = workbookPart.SharedStringTablePart.SharedStringTable
                    .Elements<SharedStringItem>().ElementAt(
                        Convert.ToInt32(cell.CellValue.Text)).InnerText;
            }

            return (text ?? string.Empty).Trim();
        }
        
        protected void ImportUsersButton_Click(object sender, EventArgs e)
        {
            Regex re = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            int importCount = 0;
            var sec = new LoginSecurity();
            if (ImportUserMatrixFile.HasFile) {
                try {
                    var workPart = SpreadsheetDocument.Open(ImportUserMatrixFile.FileContent , false).WorkbookPart;
                    var sheetData = workPart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
                    foreach (var row in sheetData.Elements<Row>()) {
                        if (row.RowIndex > 0)
                        {
                            var cells = row.Descendants<Cell>().ToList();
                            if (cells.Count >= 5) {
                                string username = ReadExcelCell(cells[0], workPart);
                                if (new Users().GetUserByIdFromUserName(username) == -1)
                                {
                                    NSurveyUserData userData = new NSurveyUserData();
                                    NSurveyUserData.UsersRow newUser = userData.Users.NewUsersRow();
                                    newUser.UserName = username.Trim();

                                    string password = "123456";

                                    newUser.PasswordSalt = sec.CreateSaltKey(5);
                                    newUser.Password = sec.CreatePasswordHash(password, newUser.PasswordSalt);
                                    string email = ReadExcelCell(cells[1], workPart);
                                    newUser.Email = email.Length > 0 && re.IsMatch(email.Trim()) ?
                                        email.Trim() : null;
                                    if (cells.Count >= 6)
                                        newUser.FirstName = ReadExcelCell(cells[5], workPart);
                                    if (cells.Count >= 7)
                                        newUser.LastName = ReadExcelCell(cells[6], workPart);
                                    userData.Users.Rows.Add(newUser);
                                    ((INSurveyUserProvider)_userProvider).AddUser(userData);
                                    if (userData.Users[0].UserId > 0)
                                    {
                                        importCount++;
                                        //TODO: add user group
                                        AddUserSettings(userData.Users[0].UserId);
                                        AddUserRoles(userData.Users[0].UserId);
                                        new Survey().AssignUserToSurvey(int.Parse(ReadExcelCell(cells[2], workPart)), userData.Users[0].UserId);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ex.Message);
                }
            }
            string[] users = "".Split('\n'); //ImportUsersTextBox.Text.Split('\n');
            
           
            //for (int i = 0; i < users.Length; i++)
            //{
            //    string[] user = users[i].Split(',');
            //    if (user.Length > 4 && user[0].Trim().Length > 0 && user[1].Trim().Length > 0)
            //    {
            //        // Check if user already exists in the db
            //        if (new Users().GetUserByIdFromUserName(user[0]) == -1)
            //        {
            //            NSurveyUserData userData = new NSurveyUserData();
            //            NSurveyUserData.UsersRow newUser = userData.Users.NewUsersRow();
            //            newUser.UserName = user[0].Trim();
                     
            //            string password = user[1].Trim();

            //            newUser.PasswordSalt =sec.CreateSaltKey(5);
            //            newUser.Password = sec.CreatePasswordHash(password, newUser.PasswordSalt);

            //            newUser.Email = user[4].Length > 0 && re.IsMatch(user[4].Trim()) ?
            //                user[4].Trim() : null;
            //            newUser.FirstName = user[3].Length > 0 ? user[3].Trim() : null;
            //            newUser.LastName = user[2].Length > 0 ? user[2].Trim() : null;
            //            userData.Users.Rows.Add(newUser);
            //            ((INSurveyUserProvider)_userProvider).AddUser(userData);
            //            if (userData.Users[0].UserId > 0) importCount++;
            //            AddUserSettings(userData.Users[0].UserId);
            //            AddUserRoles(userData.Users[0].UserId);
            //            //if (!HasSurveyAccessCheckBox.Checked)
            //            //{
            //            //    AddUserSurveys(userData.Users[0].UserId);
            //            //}
            //        }
            //    }
            //}

            MessageLabel.Visible = true;
            if(importCount>0)
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("UserImportedMessage"));
            else
            ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("NoUserImportedMessage"));
            
            BindSurveyDropDownLists();
        }

        private void AddUserSettings(int userId)
        {
            UserSettingData userSettings = new UserSettingData();
            UserSettingData.UserSettingsRow newUserSettings = userSettings.UserSettings.NewUserSettingsRow();
            newUserSettings.UserId = userId;
            
            userSettings.UserSettings.Rows.Add(newUserSettings);
            new User().AddUserSettings(userSettings);
        }

        private void AddUserRoles(int userId)
        {
            new Role().AddRoleToUser(6, userId);//6-Survey Respondents
            //foreach (ListItem item in UserRolesListBox.Items)
            //{
            //    new Role().AddRoleToUser(int.Parse(item.Value), userId);
            //}
        }

        private void AddUserSurveys(int userId)
        {
            //foreach (ListItem item in UserSurveysListBox.Items)
            //{
            //    new Survey().AssignUserToSurvey(int.Parse(item.Value), userId);
            //}
        }

        private void HasSurveyAccessCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            //SurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
            //UserSurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
        }

        private void SurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //UserSurveysListBox.Items.Add(new ListItem(SurveysListBox.SelectedItem.Text, SurveysListBox.SelectedItem.Value));
            //SurveysListBox.Items.Remove(SurveysListBox.SelectedItem);
        }

        private void UserSurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //SurveysListBox.Items.Add(new ListItem(UserSurveysListBox.SelectedItem.Text, UserSurveysListBox.SelectedItem.Value));
            //UserSurveysListBox.Items.Remove(UserSurveysListBox.SelectedItem);
        }

        private void RolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //UserRolesListBox.Items.Add(new ListItem(RolesListBox.SelectedItem.Text, RolesListBox.SelectedItem.Value));
            //RolesListBox.Items.Remove(RolesListBox.SelectedItem);

        }

        private void UserRolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //RolesListBox.Items.Add(new ListItem(UserRolesListBox.SelectedItem.Text, UserRolesListBox.SelectedItem.Value));
            //UserRolesListBox.Items.Remove(UserRolesListBox.SelectedItem);
        }

        
    }
}
