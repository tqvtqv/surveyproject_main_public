<%@ Page language="c#" MasterPageFile="Public.Master" ValidateRequest="true" AutoEventWireup="false" Inherits="Votations.NSurvey.details" Codebehind="details.aspx.cs" %>



<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <style>
        .BarDiv {
            color: white;
            background-color: #22bce0;
            height:40px;
            
        }

        .bgtable thead tr {
            background-color:gainsboro;
            
}
      .bgtable tr {
          border-style: dotted;
          border-width:2px;
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
            <table class="table table-bordered bgtable">
    <thead>
      <tr>
        <th>Tên đơn vị</th>
        <th>Đã khảo sát</th>
        <th>Chưa khảo sát</th>
        <th>Điểm</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>John</td>
        <td>
            <input type="checkbox" />
        </td>
        <td>&nbsp;</td>
        <td>99</td>
      </tr>
      <tr>
        <td>Mary</td>
        <td><input type="checkbox" /></td>
        <td>&nbsp;</td>
            <td>99</td>
      </tr>
      <tr>
        <td>July</td>
        <td><input type="checkbox" /></td>
        <td>&nbsp;</td>
            <td>99</td>
      </tr>
    </tbody>
  </table>
       </div>
       <div class="col-md-6">

            <table class="table table-bordered bgtable">
    <thead>
       <tr>
        <th>Tên đơn vị</th>
        <th>Đã khảo sát</th>
        <th>Chưa khảo sát</th>
        <th>Điểm</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>John</td>
        <td><input type="checkbox" /></td>
        <td>&nbsp;</td>
            <td>99</td>
      </tr>
      <tr>
        <td>Mary</td>
        <td><input type="checkbox" /></td>
        <td>&nbsp;</td>  <td>99</td>

      </tr>
      <tr>
        <td>July</td>
        <td><input type="checkbox" /></td>
        <td>&nbsp;</td>
            <td>99</td>
      </tr>
    </tbody>
  </table>
       </div>
   </div>
  
    
    
</asp:content>
