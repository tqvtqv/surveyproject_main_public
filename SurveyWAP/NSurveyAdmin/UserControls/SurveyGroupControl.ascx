
<%@ Register TagPrefix="uc1" TagName="SurveyListControl" Src="SurveyListControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.SurveyGroupControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" Codebehind="SurveyGroupControl.ascx.cs" %>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>


<!-- New Survey -->
        <div style="position: relative; left: 720px; width: 10px;  top: 10px; clear:none;">
            <a onmouseover='this.style.cursor="help" ' onfocus='this.blur();' href="Help/Survey General Settings.aspx"
                title="Click for more Information">
                <img alt="help" border="0" src="<%= Page.ResolveUrl("~")%>Images/small_help.gif" />
            </a>
        </div>

            <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;" title="Survey Title">
                <legend class="titleFont" style="text-align: left; margin: 0px 15px 0 15px;">
                    <asp:Literal ID="SurveyInformationTitle2" runat="server" Text=""
                        EnableViewState="False"></asp:Literal></legend>

                <br />

                <ol>
                    <li>
                        <strong>
                            <asp:Label ID="SurveyTitleLabel" AssociatedControlID="TitleTextBox" runat="server" EnableViewState="False">Title:</asp:Label></strong>
                        <div id="tooltip">

                            <asp:TextBox
                                ID="TitleTextBox"
                                runat="server"
                                Columns="60" MaxLength="90" Enabled="false">
                            </asp:TextBox>

                            <asp:Button ID="CreateSurveyButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create survey!"></asp:Button>
                        </div>

                    </li>
                </ol>
                <br />
            </fieldset>



     <asp:PlaceHolder ID="EditUi" runat="server">
         <br />
         <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;" title="Survey Settings">
             <legend class="titleFont" style="text-align: left; margin: 0px 15px 0 15px;">
                 <asp:Literal ID="SurveyInformationTitle" runat="server" Text="Survey information"
                     EnableViewState="False"></asp:Literal></legend>
             <br />
             <ol>
                 <li>
                     <strong>
                         <asp:Label ID="GroupIdLabel" runat="server" AssociatedControlID="GroupIdTextBox" EnableViewState="False">Group Id:</asp:Label></strong>
                     <asp:TextBox ID="GroupIdTextBox" 
                             runat="server" Columns="30"></asp:TextBox>
                 </li>
                 
             </ol>
             <br />
         </fieldset>
<!-- Notification Settings -->


            
     </asp:PlaceHolder>

<br />
<asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Apply changes" onclick="ApplyChangesButton_Click1"></asp:Button>
<asp:Button ID="DeleteButton" CssClass="btn btn-primary btn-xs" runat="server" Text="Delete"></asp:Button>
<br /><br />



