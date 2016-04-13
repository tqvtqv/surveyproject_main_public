/**************************************************************************************************
	Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)


	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/

namespace Votations.NSurvey.WebAdmin.UserControls
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.UserProvider;
    using Votations.NSurvey.WebAdmin;
    using Votations.NSurvey.WebAdmin.Code;
    /// <summary>
    /// Survey data CU methods
    /// </summary>
    public partial class GroupManager : UserControl
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label UserOptionTitleLabel;
		protected System.Web.UI.WebControls.Button ApplyChangesButton;
		
		public event EventHandler OptionChanged;

        Votations.NSurvey.SQLServerDAL.User UsersData;
        /// <summary>
        /// Id of the user to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int GroupId
        {
			get { return (ViewState["GroupId"]==null) ? -1 : int.Parse(ViewState["GroupId"].ToString()); }
            set { 
                ViewState["GroupId"] = value;
                if (value < 0)
                    SwitchToCreationMode();
                else
                    SwitchToEditionMode(); 
            }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			MessageLabel.Visible = false;
			LocalizePage();
            UsersData = new SQLServerDAL.User();
            // Check if any answer type id has been assigned
            if (GroupId == -1)
			{
				SwitchToCreationMode();
			}
			else
			{
				SwitchToEditionMode();
			}
            if (!Page.IsPostBack)
                BindGrid();
		}

        private void BindGrid()
        {
            int parentId = 0;
            int.TryParse(ParentIdTextBox.Text, out parentId);
            if (parentId != 0)
                gvGroups.DataSource = UsersData.GetChildGroups(parentId);
            else
                gvGroups.DataSource = UsersData.GetChildGroups(null);
            gvGroups.DataBind();
        }

        private void LocalizePage()
		{
			//CreateNewUserButton.Text = ((PageBase)Page).GetPageResource("CreateNewUserButton");
			ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
			//DeleteUserButton.Text = ((PageBase)Page).GetPageResource("DeleteUserButton");
			//UserPasswordLabel.Text = ((PageBase)Page).GetPageResource("UserPasswordLabel");
			//UserNameLabel.Text = ((PageBase)Page).GetPageResource("UserNameLabel");
			//UserFirstNameLabel.Text = ((PageBase)Page).GetPageResource("UserFirstNameLabel");
			//UserLastNameLabel.Text = ((PageBase)Page).GetPageResource("UserLastNameLabel");
			//UserEmailLabel.Text = ((PageBase)Page).GetPageResource("UserEmailLabel");
			//AssignAllSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignAllSurveysLabel");
			//GroupListLabel.Text = ((PageBase)Page).GetPageResource("UserSurveyAssignedLabel");
			//UnAssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("UnAssignedSurveysLabel");
			//AssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignedSurveysLabel");
			//RolesLabel.Text = ((PageBase)Page).GetPageResource("RolesLabel");
			//AvailableRolesLabel.Text = ((PageBase)Page).GetPageResource("AvailableRolesLabel");
			//UserRolesLabel.Text = ((PageBase)Page).GetPageResource("UserRolesLabel");
			//UserIsAdministratorLabel.Text = ((PageBase)Page).GetPageResource("UserIsAdministratorLabel");
		}


		/// <summary>
		/// Setup the control in creation mode
		/// </summary>
		private void SwitchToCreationMode()
		{
			//if (_userProvider is INSurveyUserProvider)
			//{
                // Creation mode
                GroupOptionTitleLabel.Text = "New group";
                CreateNewGroupButton.Visible = true;
				ApplyChangesButton.Visible = false;
				DeleteGroupButton.Visible = false;
				//ExtendedSettingsPlaceHolder.Visible = false;
				//NSurveyUserPlaceHolder.Visible = true;
                this.Visible = true;
			//}
			//else if (this.Visible)
			//{
			//	throw new ApplicationException("Specified user provider doesn't support creation of users");
			//}

			//HasSurveyAccessCheckBox.AutoPostBack = false;
		}

		/// <summary>
		/// Setup the control in edition mode
		/// </summary>
		private void SwitchToEditionMode()
		{
			DeleteGroupButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteUserConfirmationMessage")+ "')== false) return false;");

            GroupOptionTitleLabel.Text = "Edit group";
			CreateNewGroupButton.Visible = false;
			ApplyChangesButton.Visible = true;
            DeleteGroupButton.Visible = true;// !(UserId == ((PageBase)Page).NSurveyUser.Identity.UserId) && _userProvider is INSurveyUserProvider;
            if (!Page.IsPostBack)
                BindFields();

        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        public void BindFields()
        {
            ParentIdTextBox.Text = string.Empty;
            GroupNameTextBox.Text = string.Empty;
            if (GroupId < 0)
            {
                ViewState["GroupId"] = string.Empty;

                return;
            }

            DataSet groups = UsersData.GetGroupById(GroupId);
            if (groups.Tables[0].Rows.Count > 0)
            {
                if (groups.Tables[0].Rows[0]["ParentId"] != null)
                    ParentIdTextBox.Text = groups.Tables[0].Rows[0]["ParentId"].ToString();
                if (groups.Tables[0].Rows[0]["GroupName"] != null)
                    GroupNameTextBox.Text = groups.Tables[0].Rows[0]["GroupName"].ToString();
            }

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			//this.HasSurveyAccessCheckBox.CheckedChanged += new System.EventHandler(this.HasSurveyAccessCheckBox_CheckedChanged);
			//this.SurveysListBox.SelectedIndexChanged += new System.EventHandler(this.SurveysListBox_SelectedIndexChanged);
			//this.UserSurveysListBox.SelectedIndexChanged += new System.EventHandler(this.UserSurveysListBox_SelectedIndexChanged);
			//this.RolesListBox.SelectedIndexChanged += new System.EventHandler(this.RolesListBox_SelectedIndexChanged);
			//this.UserRolesListBox.SelectedIndexChanged += new System.EventHandler(this.UserRolesListBox_SelectedIndexChanged);
			this.CreateNewGroupButton.Click += new System.EventHandler(this.CreateGroupButton_Click);
			this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
			this.DeleteGroupButton.Click += new System.EventHandler(this.DeleteGroupButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CreateGroupButton_Click(object sender, System.EventArgs e)
		{
			if (ValidateFieldOptions())
			{
                DataSet ds = new DataSet();
                DataTable tbl = new DataTable();
                tbl.Columns.Add("Id");
                tbl.Columns.Add("GroupName");
                tbl.Columns.Add("ParentId");
                DataRow row = tbl.NewRow();
                row["Id"] = GroupId;
                row["GroupName"] = GroupNameTextBox.Text;
                if (!String.IsNullOrEmpty(ParentIdTextBox.Text))
                    row["ParentId"] = ParentIdTextBox.Text;

                UsersData.CreateGroup(row);
                UINavigator.NavigateToGroupManager(((PageBase)Page).getSurveyId(),((PageBase)Page).MenuIndex);
			}

		}
        private void ApplyChangesButton_Click(object sender, System.EventArgs e)
        {
            if (ValidateFieldOptions())
            {
                DataSet ds = new DataSet();
                DataTable tbl = new DataTable();
                tbl.Columns.Add("Id");
                tbl.Columns.Add("GroupName");
                tbl.Columns.Add("ParentId");
                DataRow row = tbl.NewRow();
                row["Id"] = GroupId;
                row["GroupName"] = GroupNameTextBox.Text;
                if (!String.IsNullOrEmpty(ParentIdTextBox.Text))
                    row["ParentId"] = ParentIdTextBox.Text;

                UsersData.UpdateGroup(row);
                // Notifiy containers that data has changed
                OnOptionChanged();
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("UserUpdatedMessage"));
            }
        }

        private void DeleteGroupButton_Click(object sender, System.EventArgs e)
        {
            UsersData.DeleteGroupById(GroupId);
            GroupId = -1;
            Visible = false;
            OnOptionChanged();
            UINavigator.NavigateToUserManager(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex);
        }
        /// <summary>
        /// Validate all fields to make sure 
        /// no errors has occured
        /// </summary>
        private bool ValidateFieldOptions()
		{		
			//if (!(_userProvider is INSurveyUserProvider))
			//{
			//	return true;
			//}

			if (GroupNameTextBox.Text.Length == 0)
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, "Chưa nhập tên group");
				RePopulatePasswordBox();
				return false;
			}
            int parentid = 0;
            int.TryParse(ParentIdTextBox.Text, out parentid);
            int groupid = -1;
            DataSet group;
            if (parentid == 0)
                group = UsersData.GetGroupByName(GroupNameTextBox.Text, null);
            else
                group = UsersData.GetGroupByName(GroupNameTextBox.Text, parentid);

            if (group != null && group.Tables.Count > 0 && group.Tables[0].Rows.Count > 0) {
                try {
                    groupid = Convert.ToInt32(group.Tables[0].Rows[0]["Id"]);
                } catch (Exception ex) { groupid = -1; }
            }
                

            if (groupid != -1 && groupid != GroupId)
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, "Group đã tồn tại");
				RePopulatePasswordBox();
				return false;
			}
			return true;
		}

		private void RePopulatePasswordBox()
		{
			//PasswordTextBox.Attributes.Add("value", PasswordTextBox.Text);
		}
        public void OnGroupEdit(Object sender, CommandEventArgs e)
        {
            switch (e.CommandName) {
                case "GroupSelect":
                    ParentIdTextBox.Text = e.CommandArgument.ToString();
                    BindGrid();
                    break;
                case "GroupEdit":
                    GroupId = int.Parse(e.CommandArgument.ToString());
                    SwitchToEditionMode();
                    break;
            }
            
        }
        public void gvGroups_PageIndexChanged(Object sender, EventArgs e)
        {
            BindGrid();
        }

        public void gvGroups_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvGroups.PageIndex = e.NewPageIndex;
        }

        protected void OnOptionChanged()
		{
			if (OptionChanged != null)
			{
				OptionChanged(this, EventArgs.Empty);
			}
		}

		

		private void HasSurveyAccessCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			//SurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
			//UserSurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
		}

		private void SurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//new Survey().AssignUserToSurvey(int.Parse(SurveysListBox.SelectedValue), GroupId);
			//BindSurveyDropDownLists();
		}

		private void UserSurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//new Survey().UnAssignUserFromSurvey(int.Parse(UserSurveysListBox.SelectedValue), GroupId);
			//BindSurveyDropDownLists();
		}

		private void RolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//new Role().AddRoleToUser(int.Parse(RolesListBox.SelectedValue), GroupId);
			//BindSurveyDropDownLists();
		}

		private void UserRolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//new Role().DeleteUserRole(int.Parse(UserRolesListBox.SelectedValue), GroupId);
			//BindSurveyDropDownLists();
		}

		//private IUserProvider _userProvider = UserFactory.Create();

        
	}
}
