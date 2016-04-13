<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserImportExcel.ascx.cs" Inherits="Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.UserImportExcel" %>
<%@ Register TagPrefix="uc1" TagName="UsersOptionsControl" Src="UsersOptionsControl.ascx" %>


            <div style="position: absolute; width: 650px; text-align: center; margin-left: 57px; top: 15px;">
 <asp:Label ID="MessageLabel" runat="server"  CssClass="ErrorMessage" Visible="False"></asp:Label>
                </div>

 <br />
            <fieldset style="width: 750px; margin-left: -5px;">
    <legend class="titleFont" style="margin: 0px 15px 0 15px; text-align: left;">
        <asp:Label ID="ImportUsersTitle" runat="server">Import user matrix</asp:Label>
    </legend>
    <br />
            <asp:Label ID="ImportUserLabel" AssociatedControlID="ImportUserMatrixFile" runat="server" EnableViewState="False">Chọn file excel: </asp:Label>
            <asp:FileUpload ID="ImportUserMatrixFile" runat="server" AllowMultiple="false" accept="excel/xlsx"  />
                <asp:RegularExpressionValidator ID="uplValidator" runat="server" 
                    ControlToValidate="ImportUserMatrixFile"
                    CssClass="ErrorMessage"
                    ErrorMessage=".xlsx allowed only" 
                    ValidationExpression="(.+\.([Xx][Ll][Ss][Xx]))">
                   
                </asp:RegularExpressionValidator>
                
            <asp:Button ID="ImportUsersButton" CssClass="btn btn-primary btn-xs bw" runat="server" Text="Import users" OnClick="ImportUsersButton_Click" ></asp:Button>
 <br />
</fieldset>