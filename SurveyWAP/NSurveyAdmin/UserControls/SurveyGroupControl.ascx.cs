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
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Resources;
    using Microsoft.VisualBasic;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Web;
    using Votations.NSurvey.WebAdmin.NSurveyAdmin;
    using System.Data;    /// <summary>
                          /// Survey data CU methods
                          /// </summary>
    public partial class SurveyGroupControl : UserControl
    {
        public event EventHandler OptionChanged;
        
        // protected System.Web.UI.WebControls.Literal SettingsTitleHelp;

        /// <summary>
        /// Id of the survey to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int SurveyId
        {
            get { return ((PageBase)Page).getSurveyId(); }
            set { ((PageBase)Page).SurveyId = value; }
        }

        public bool isCreationMode { get; set; }
        public bool isListMode { get; set; }

        private void Page_Load(object sender, System.EventArgs e)
        {

            LocalizePage();

            MessageLabel.Visible = false;
            DeleteButton.Attributes.Add("onClick",
                            "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("DeleteSurveyConfirmationMessage") + "')== false) return false;");


            // Check if any survey has been assigned
            if (isCreationMode || isListMode)
            {
                SwitchToCreationMode();
            }
            else
            {
                SwitchToEditionMode();
            }

            SetupSecurity();
        }

        private void SetupDeleteConfirm()
        {
        }
        private void SetupSecurity()
        {
            if (!isListMode && !isCreationMode)
            {
                ((PageBase)Page).CheckRight(NSurveyRights.AccessSurveySettings, true);
                this.ApplyChangesButton.Enabled = ((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.ApplySurveySettings);
            }
        }
        private void LocalizePage()
        {
            SurveyInformationTitle.Text = ((PageBase)Page).GetPageResource("SurveyInformationTitle");
            SurveyInformationTitle2.Text = ((PageBase)Page).GetPageResource("SurveyInformationTitle2");
            SurveyTitleLabel.Text = ((PageBase)Page).GetPageResource("SurveyTitleLabel");
            
            ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
            DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
            CreateSurveyButton.Text = ((PageBase)Page).GetPageResource("CreateSurveyButton");
            
            // QuestionNumberingCheckbox.Attributes.Add("title", ((PageBase)Page).GetPageHelpfiles("SettingsQnumberingHelp")) ;
        }

        /// <summary>
        /// Setup the control in creation mode
        /// </summary>
        private void SwitchToCreationMode()
        {
            // Creation mode
            
            CreateSurveyButton.Visible = true;
            ApplyChangesButton.Visible = false;
            DeleteButton.Visible = false;
            EditUi.Visible = false;

        }

        /// <summary>
        /// Setup the control in edition mode
        /// </summary>
        private void SwitchToEditionMode()
        {
            CreateSurveyButton.Visible = false;
            ApplyChangesButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.ApplySurveySettings, false);
            DeleteButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.DeleteSurvey, false);

            EditUi.Visible = true;

            if (!IsPostBack)
            {
                BindFields();
            }
        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        private void BindFields()
        {
            // Retrieve the survey data
            SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, "");
            SurveyData.SurveysRow survey = surveyData.Surveys[0];

            // Assigns the retrieved data to the correct fields
            TitleTextBox.Text = survey.Title;
            ViewState["ThanksMessage"] = survey.ThankYouMessage;
            DataSet surveygroupData = new Surveys().GetSurveyGroup(SurveyId);
            if (surveygroupData != null && surveygroupData.Tables.Count > 0 && surveygroupData.Tables[0].Rows.Count > 0)
                // Assigns the retrieved data to the correct fields
                GroupIdTextBox.Text = (String)surveygroupData.Tables[0].Rows[0]["GroupId"];
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
            //this.EntryNotificationDropdownlist.SelectedIndexChanged += new System.EventHandler(this.EntryNotificationDropdownlist_SelectedIndexChanged);
            this.CreateSurveyButton.Click += new System.EventHandler(this.CreateSurveyButton_Click);
            this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        

        /// <summary>
        /// Apply the survey data changes to the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyChangesButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Creates a new entity and assign
                // the updated values
                DataTable tbl = new DataTable();
                tbl.Columns.Add("SurveyId");
                tbl.Columns.Add("GroupId");
                DataRow row = tbl.NewRow();
                row["SurveyId"] = SurveyId;
                row["GroupId"] = GroupIdTextBox.Text;
                // Update the DB
                new Surveys().UpdateSurveyGroup(row);

                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyUpdatedMessage"));
                MessageLabel.Visible = true;

                ((Wap)this.Page.Master.Master).isTreeStale = true;
                ((Wap)this.Page.Master.Master).RebuildTree();

                // Let the subscribers know that something changed
                OnOptionChanged();
            }

            catch (SurveyExistsFoundException ex)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyExistsInFolder"));
                MessageLabel.Visible = true;
            }
        }

        private void CreateSurveyButton_Click(object sender, System.EventArgs e)
        {
            // Creates a new entity and assign
            // the new values
            //SurveyData surveyData = new SurveyData();
            //SurveyData.SurveysRow surveyRow = surveyData.Surveys.NewSurveysRow();

            //if (TitleTextBox.Text.Length == 0)
            //{
            //    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingSurveyTitleMessage"));
            //    MessageLabel.Visible = true;
            //    return;
            //}

            //surveyRow.Title = TitleTextBox.Text;

            //surveyRow.Activated = false;
            //surveyRow.Archive = false;
            //surveyRow.AccessPassword = string.Empty;
            //surveyRow.SurveyDisplayTimes = 0;
            //surveyRow.ResultsDisplayTimes = 0;
            //surveyRow.SetOpenDateNull();
            //surveyRow.SetCloseDateNull();
            //surveyRow.CreationDate = DateTime.Now;
            //surveyRow.IPExpires = 1440;
            //surveyRow.CookieExpires = 1440;
            //surveyRow.OnlyInvited = false;
            //surveyRow.Scored = false;
            //surveyRow.QuestionNumberingDisabled = false;
            //surveyRow.FolderId = ((PageBase)Page).SelectedFolderId;
            //surveyRow.ProgressDisplayModeID = (int)ProgressDisplayMode.PagerNumbers;
            //surveyRow.ResumeModeID = (int)ResumeMode.NotAllowed;
            //surveyData.Surveys.AddSurveysRow(surveyRow);

            try
            {
                // Add the survey in the DB
                DataTable tbl = new DataTable();
                tbl.Columns.Add("SurveyId");
                tbl.Columns.Add("GroupId");
                DataRow row = tbl.NewRow();
                row["SurveyId"] = SurveyId;
                row["GroupId"] = GroupIdTextBox.Text;
                // Update the DB
                new Surveys().UpdateSurveyGroup(row);
                //This messes up the tree astructure etc so Stay where you are
                UINavigator.NavigateToSurveyBuilder(SurveyId, 4);
            }
            catch (SurveyExistsFoundException ex)
            {
                string x = ex.Message;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyExistsInFolder"));
                MessageLabel.Visible = true;
            }
        }

        /// <summary>
        /// If a valid user is logged in assign the survey 
        /// to his account
        /// </summary>
        private void AssignSurveyToUser(int surveyId)
        {
            // Can we assign the survey to the current user
            if (!((PageBase)Page).IsSingleUserMode(false))
            {
                new Survey().AssignUserToSurvey(surveyId,
                    ((PageBase)Page).NSurveyUser.Identity.UserId);
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            // Delete survey from the DB
            DataTable tbl = new DataTable();
            tbl.Columns.Add("SurveyId");
            tbl.Columns.Add("GroupId");
            DataRow row = tbl.NewRow();
            row["SurveyId"] = SurveyId;
            row["GroupId"] = GroupIdTextBox.Text;
            // Update the DB
            new Surveys().UpdateSurveyGroup(row);
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyDeletedMessage"));

            MessageLabel.Visible = true;
            ((PageBase)Page).SurveyDeleteActions(SurveyId);
            UINavigator.NavigateToSurveyOptions(-1, ((PageBase)Page).MenuIndex);
        }

        protected void OnOptionChanged()
        {
            if (OptionChanged != null)
            {
                OptionChanged(this, EventArgs.Empty);
            }
        }

        private void CloneButton_Click(object sender, System.EventArgs e)
        {
            SurveyData clonedSurvey = new Survey().CloneSurvey(SurveyId);

            //Retrieve the new survey ID
            SurveyId = clonedSurvey.Surveys[0].SurveyId;

            // Assign the cloned survey to the user
            AssignSurveyToUser(SurveyId);

            // Update the form fields
            BindFields();
            ((Wap)Page.Master.Master).isTreeStale = true;
            ((Wap)Page.Master.Master).RebuildTree();
            // Let the subscribers know that something has changed
            OnOptionChanged();

        }

        private void ExportSurveyButton_Click(object sender, System.EventArgs e)
        {
            NSurveyForm surveyForm = null;


            Response.Charset = "utf-8";
            Response.ContentType = "application/octet-stream";

            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"SurveyExport" + SurveyId + ".xml\"");

            surveyForm = new Surveys().GetFormForExport(SurveyId);
            surveyForm.WriteXml(Response.OutputStream, System.Data.XmlWriteMode.IgnoreSchema);

            Response.End();
        }

        
    }
}
