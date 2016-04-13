<%@ Page language="c#" MasterPageFile="Public.Master" ValidateRequest="true" AutoEventWireup="false" Inherits="Votations.NSurvey.details" Codebehind="details.aspx.cs" %>



<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <style>
        .BarDiv {
            color: white;
            background-color: #22bce0;
            height:40px;
            
        }

        thread tr {
            background-color:gray;
        }
    </style>
    <div class="row" style="margin-top:-20px;">
        <div class="col-md-12 BarDiv">
           <b>VNPT Tỉnh/TP </b>- Miền Bắc
        </div>
    </div>
    <br />
   <div class="row">
       <div class="col-md-6">
            <table class="table table-bordered">
    <thead>
      <tr>
        <th>Firstname</th>
        <th>Lastname</th>
        <th>Email</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>John</td>
        <td>Doe</td>
        <td>john@example.com</td>
      </tr>
      <tr>
        <td>Mary</td>
        <td>Moe</td>
        <td>mary@example.com</td>
      </tr>
      <tr>
        <td>July</td>
        <td>Dooley</td>
        <td>july@example.com</td>
      </tr>
    </tbody>
  </table>
       </div>
       <div class="col-md-6">

            <table class="table table-bordered">
    <thead>
      <tr>
        <th>Firstname</th>
        <th>Lastname</th>
        <th>Email</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>John</td>
        <td>Doe</td>
        <td>john@example.com</td>
      </tr>
      <tr>
        <td>Mary</td>
        <td>Moe</td>
        <td>mary@example.com</td>
      </tr>
      <tr>
        <td>July</td>
        <td>Dooley</td>
        <td>july@example.com</td>
      </tr>
    </tbody>
  </table>
       </div>
   </div>
  
    
    
</asp:content>
