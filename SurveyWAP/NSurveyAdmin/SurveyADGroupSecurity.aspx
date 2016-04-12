<%@ Page Title="" Language="C#" MasterPageFile="~/NSurveyAdmin/MsterPageTabs.master"
    AutoEventWireup="true" CodeBehind="SurveyADGroupSecurity.aspx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.SurveyADGroupSecurity" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mainBody" class="mainBody contentHolder ps-container">
        <div id="Panel" class="Panel content">

    <script type="text/javascript">
        function ConfirmIPRangeDelete() {
            return confirm('Are you sure you want to delete this AD Group?');
        }
    </script>

            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>
     <br />
         <fieldset style="width: 750px; margin-left: 12px; margin-top: 15px;">
              
             <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align:left;">
                    <asp:Literal ID="literalADGroupTitle" runat="server" EnableViewState="False"></asp:Literal>
             </legend> 
              <br />
                      <ol>
     <li>
                <asp:Label ID="lblAdGroupNamePrompt" AssociatedControlID="txtAdGroupName" runat="server" Text="Nhập tên đơn vị:" />&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="txtAdGroupName"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnAddADGroup" Text="Thêm" CssClass="btn btn-primary btn-xs bw"
                    OnClick="btnAddADGroup_Click" /><br />
</li>
                          <li>
                

    
                <asp:ListView ID="lvADGroups" runat="server">
                    <LayoutTemplate>
                        <table  class="BorderedTable" width="100%" cellspacing="0">

                            <asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>

                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td  class="<%# (Container.DisplayIndex/2) % 2 == 0 ? "" : "AlternateStyle" %>"
                            style="padding-right: 6px;">
                            <table>
                                <td Width="20px">
                                    <asp:Literal ID="ltADGroupId" runat="server" Visible="false" Text='<%# Eval("Id") %>'></asp:Literal>
                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreationDate" ReadOnly="true" Text='<%#Eval("GroupName","{0:d}")%>'
                                        runat="server"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox Width="190px" ID="txtToken" ReadOnly="true" Text='<%#Eval("FilterPhase")%>'
                                        runat="server"> </asp:TextBox>
                                </td>
                                
                            </table>
                        </td>
                    </ItemTemplate>
             
                </asp:ListView>

                                   </li>
                          <li>
            
                <asp:Button runat="server" CssClass="btn btn-primary btn-xs bw" ID="btnDeleteSelected" OnClientClick="return ConfirmSelectedTokenDelete();"
                    Text="Delete selected" OnClick="btnDeleteSelected_Click" />
                <%--<asp:Button CssClass="btn btn-primary btn-xs bw" runat="server" OnClientClick="return ConfirmAllTokenDelete();"
                    ID="btnDeleteAll" Text="Delete all" OnClick="btnDeleteAll_Click" />--%>
                
            <br />
       </li>
  </ol>
                    <br />
                    </fieldset>

</div></div></asp:Content>
