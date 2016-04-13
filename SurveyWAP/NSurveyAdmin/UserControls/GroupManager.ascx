<%@ Control Language="c#" AutoEventWireup="false" Inherits="Votations.NSurvey.WebAdmin.UserControls.GroupManager"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="GroupManager.ascx.cs" %>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
<br />
            <fieldset style="width:750px; margin-left:-5px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                <asp:Label ID="GroupOptionTitleLabel" runat="server">New Group</asp:Label>
                    </legend>
                                <br />
                <ul>
                <asp:PlaceHolder ID="NSurveyUserPlaceHolder" runat="server">
                    <li>
                        <asp:Label ID="ParentIdLabel" AssociatedControlID="ParentIdTextBox" runat="server" EnableViewState="False" Text="Parent Id:"></asp:Label>
                        <asp:TextBox ID="ParentIdTextBox" runat="server" Enabled="False"></asp:TextBox>
                    </li>
<li>
                     <asp:Label ID="GroupNameLabel" AssociatedControlID="GroupNameTextBox" runat="server" EnableViewState="False" Text="Group name:"></asp:Label>
                            <asp:TextBox ID="GroupNameTextBox" runat="server"></asp:TextBox>

                      </li>
                    
                </asp:PlaceHolder>

  
  </ul> 
                         <asp:Button ID="CreateNewGroupButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Create group"></asp:Button>

                        <asp:Button ID="ApplyChangesButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Apply changes"></asp:Button>
                        <asp:Button ID="DeleteGroupButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Delete group" Visible="False"></asp:Button>

                    </fieldset>

<br /><br />

                <asp:PlaceHolder ID="ExtendedSettingsPlaceHolder" runat="server">

        <fieldset style="width:750px; margin-left:-5px;">
                <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                                <asp:Literal ID="GroupListLabel" runat="server" Text="Group list :" EnableViewState="False"></asp:Literal>
                    </legend>
            <asp:GridView runat="server" Width="100%" ID="gvGroups" AutoGenerateColumns="False" AllowPaging="true" EnableViewState="true"
                            OnPageIndexChanged="gvGroups_PageIndexChanged" OnPageIndexChanging="gvGroups_PageIndexChanging"
                            PageSize="20" AlternatingRowStyle-BackColor="#FFF6BB" ShowFooter="True" FooterStyle-BackColor="#FFDF12" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="#E2E2E2" >

                            <PagerSettings Visible="true" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10"
                        NextPageText=">" PreviousPageText="<" />
                    <PagerStyle ForeColor="Black" Font-Size="X-Small"  HorizontalAlign="Center" BackColor="#C6C3C6" VerticalAlign="Bottom"
                        Width="200px" Height="5px"></PagerStyle>
                            <Columns>
                        <asp:TemplateField ItemStyle-Width="9%" ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="80px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6" >
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lbl1" Text='Group id' /></HeaderTemplate>
                            <ItemTemplate>
                         <!--       <asp:Label runat="server" Text='<%#(Eval("Id")) %>' /> -->
                             <asp:LinkButton Text='<%#(Eval("Id")) %>' runat="server" ToolTip="List child groups" OnCommand="OnGroupEdit"
                                    CommandName="GroupSelect" CommandArgument='<%#(Eval("Id")) %>' CssClass="hyperlink" />
                                </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='Group name' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(Eval("GroupName")) %>' /></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="17%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="80px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='ParentId' /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#(Eval("ParentId")) %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="9%"  ItemStyle-BorderWidth="1px" ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="#E2E2E2" HeaderStyle-Width="80px" HeaderStyle-BackColor="#e2e2e2" HeaderStyle-BorderColor="#e2e2e2" HeaderStyle-ForeColor="#5720C6"  >
                            <HeaderTemplate>
                                <asp:Label runat="server" Text='Edit' /></HeaderTemplate>
                            <ItemTemplate>
                            <asp:ImageButton runat="server" ToolTip="Click to Edit" ImageUrl="~/Images/edit_pen.gif" Width="16" Height="16" OnCommand="OnGroupEdit"
                                    CommandName="GroupEdit" CommandArgument='<%#(Eval("Id")) %>' />
                           </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                    </fieldset>

                </asp:PlaceHolder>

