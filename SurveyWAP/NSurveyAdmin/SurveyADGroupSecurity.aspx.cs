using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votation.NSurvey.LDAPProvider;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveyADGroupSecurity : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sId = SurveyId;//This causes a redirection if survey id is not set
            UITabList.SetSecurityTabs((MsterPageTabs)Page.Master, UITabList.SecurityTabs.ADGroup);
            SetupSecurity();
            LocalizePage();
            if (!IsPostBack)
            {
              
                BindFields();
            }

        }

        private void LocalizePage()
        {
            literalADGroupTitle.Text= GetPageResource("ADGroupSecurity");
            btnDeleteSelected.Text = GetPageResource("Delete");
            btnAddADGroup.Text = GetPageResource("Add");
            
        }
        protected void BindFields()
        {
            DataSet data = new Survey().GetAllAdGroupDetails(((PageBase)Page).SurveyId);
            DataView dv1 = data.DefaultViewManager.CreateDataView(data.Tables[0]);


            // when no results found we add new empty row and clear it cells
            if (dv1.Count == 0)
                dv1.AddNew();

            lvADGroups.DataSource = dv1;

            lvADGroups.DataBind();


        }
        
        private void SetupSecurity()
        {
            CheckRight(Data.NSurveyRights.TokenSecurityRight, true);
        }


        

        private void InsertUpdateIPRange(bool isInsert,int? surveyIPRangeId,int surveyId, string IPStart, string IPEnd)
        {

            SurveyIPRangeData data = new SurveyIPRangeData();

            SurveyIPRangeData.SurveyIPRangeRow row = data.SurveyIPRange.NewSurveyIPRangeRow();
            row.SurveyId = surveyId;
            row.IPStart = IPStart;
            row.IPEnd = IPEnd;

            row.SurveyIPRangeId = surveyIPRangeId ?? 0;
               
            data.SurveyIPRange.Rows.Add(row);
            if (isInsert)
                new SurveyIPRange().AddNewSurveyIPRange(data);
            new SurveyIPRange().UpdateIPRange(data);

        }
        private string assembleIpString(string controlNamePrefix, GridViewRow row)
        {
            string retval = string.Empty;
            for (int i = 1; i <= 4; i++)
                retval += ((TextBox)row.FindControl(controlNamePrefix + i.ToString())).Text + ".";
            return retval.TrimEnd(new char[] { '.' });
        }
        private void InsertIPRange(GridViewRow row)
        {
            string startIp = assembleIpString("txtIpStartNew", row);
            string endIp = assembleIpString("txtIpEndNew", row);
            InsertUpdateIPRange(true,null,((PageBase)Page).SurveyId, startIp, endIp);
        }
        private void UpdateIPRange(GridViewRow row)
        {
            string startIp = assembleIpString("txtIpStart", row);
            string endIp = assembleIpString("txtIpEnd", row);
            int surveyIPRangeID = Convert.ToInt32(lvADGroups.DataKeys[row.RowIndex].Value.ToString());
            InsertUpdateIPRange(false,surveyIPRangeID,((PageBase)Page).SurveyId, startIp, endIp);
        }
        private void DeleteIPRange(GridViewRow row)
        {
            int surveyIPRangeID = Convert.ToInt32(lvADGroups.DataKeys[row.RowIndex].Value.ToString());
            new SurveyIPRange().DeleteSurveyIPRangeById(surveyIPRangeID);
        }


        protected void btnAddADGroup_Click(object sender, EventArgs e)
        {
            var ou = txtAdGroupName.Text.Trim();
            var ldapHelper = LDAPFactory.Create();
            var results = ldapHelper.SearchOU(ou);
            if (results != null && results.Count() > 0)
            {
                var list = results.Select(x => new SurveyADGroupDetail { });
                new Survey().AddADGroupMultiple(results.Select(x => new SurveyADGroupDetail
                {
                    AddInId = this.SurveyId,
                    SurveyId = this.SurveyId,
                    FilterPhase = x,
                    GroupName = ldapHelper.GetFirstPath(x, false)
                }));
            }
            else ShowErrorMessage(MessageLabel, "Không tìm thấy đơn vị trong LDAP");
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<int> deleteCandidates = new List<int>();

            foreach (var row in lvADGroups.Items)
                if (((CheckBox)row.FindControl("chkDelete")).Checked)
                    deleteCandidates.Add(Convert.ToInt32(((Literal)row.FindControl("ltADGroupId")).Text));

            if (deleteCandidates.Count > 0)
            {
                //new Survey().DeleteSurveyTokensByIdMultiple(deleteCandidates);
                BindFields();
            }
        }
    }
}