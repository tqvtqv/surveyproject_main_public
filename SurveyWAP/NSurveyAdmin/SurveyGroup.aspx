<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="UserControls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="UserControls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SurveyGroupControl" Src="UserControls/SurveyGroupControl.ascx" %>
<%@ Page language="c#" MasterPageFile="MsterPageTabs.master"   ValidateRequest="false" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.SurveyGroup" Codebehind="SurveyGroup.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

            <uc1:SurveyGroupControl ID="SurveyOption" runat="server"></uc1:SurveyGroupControl>
        </div>

    </div>
</asp:Content>

