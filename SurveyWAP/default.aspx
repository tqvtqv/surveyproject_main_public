<%@ Page language="c#" MasterPageFile="Public.Master" ValidateRequest="true" AutoEventWireup="false" Inherits="Votations.NSurvey.default" Codebehind="~/default.aspx.cs" %>
 


<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <style>
         .divText {
       
    color:white;margin-top:8px;
    
    }
         .DivTittle{
             color:#348da0;
             font-weight:bold;

         }
          .DivBody{
          margin-left:20px;


         }
          .imageDiv{
              max-width: 100%;
                  width: 100%;
                    height: auto;
          }
</style>
   <div style="margin-top:-18px;"><img src="Images/NewBanner.png" class="imageDiv"></img></div>
    <div class="row" style="margin-top:10px;">
      
    <div class="col-md-3">
        <div style="background-color:#4dcde8;align-items:center" class="text-center"><img src="Images/icon-bcn.jpg" class="pull-left"/><label class="text-uppercase divText">Ban chức năng</label><div class="clearfix"></div></div>
     <div class="panel panel-default">
  <div class="panel-body" style="min-height:280px;max-height:280px;">
      Ban chất lượng<br />
      Ban Công nghệ - mạng<br />
      Ban IT&VAS <br />
      Ban hợp tác quốc tế <br />
      Ban kinh doanh  <br />
      Ban kế hoạch - đầu tư <br />
      Ban Kế toán - tài chính <br />
      Ban nhân sự <br />
      Ban Pháp chế - thanh tra <br />
      Ban phát triển thị trường <br />
      Ban QLDA  <br />
     
  </div>
         
      <div class="pull-right" style="margin-top:-25px;"> Xem tiếp <span class="glyphicon glyphicon-forward"></span>&nbsp;</div>
</div>
    </div>
   <div class="col-md-3">
        <div style="background-color:#f8901f;align-items:center;" class="text-center vcenter"><img src="Images/icon-vnptTinhTp.jpg" class="pull-left"/><label class="text-uppercase divText">VNPT Tỉnh/TP</label><div class="clearfix"></div></div>
     <div class="panel panel-default">
   <div class="panel-body" style="min-height:280px;max-height:280px;">
   <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Bắc</div>
    <div class="DivBody">
        Viễn thông Hà nội<br />
        Viễn thông Thái Bình<br />
        Viễn thông Hà Nam


    </div>

        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Nam</div>
    <div class="DivBody">
        Viễn thông Hồ Chí Minh<br />
        Viễn thông Tiền Giang<br />
        Viễn thông Trà Vinh


    </div>
        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Trung</div>
    <div class="DivBody">
        Viễn thông Huế<br />
        Viễn thông Quảng Bình<br />
        Viễn thông Nghệ An


    </div>

   </div>
          
      <div class="pull-right" style="margin-top:-25px;"> Xem tiếp <span class="glyphicon glyphicon-forward"></span>&nbsp;</div>
</div>
    </div>
    <div class="col-md-3">
        <div style="background-color:#56c253;align-items:center;" class="text-center vcenter"><img src="Images/icon-ctydoc.jpg" class="pull-left"/><label class="text-uppercase divText">Công ty dọc</label><div class="clearfix"></div></div>
     <div class="panel panel-default">
  <div class="panel-body"  style="min-height:280px;max-height:280px;">


       <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> VNPT Net</div>
    <div class="DivBody">
      http://survey.vnpt.vn/test.aspx
       

    </div>

        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> VNPT VNP</div>
    <div class="DivBody">
      http://survey.vnpt.vn/test.aspx
       

    </div>
        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> VNPT Media</div>
    <div class="DivBody">
      http://survey.vnpt.vn/test.aspx
       

    </div>
        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> VNPT I</div>
    <div class="DivBody">
      http://survey.vnpt.vn/test.aspx
       

    </div>

  </div>
                 
      <div class="pull-right" style="margin-top:-25px;"> Xem tiếp <span class="glyphicon glyphicon-forward"></span>&nbsp;</div>
</div>
    </div>
          <div class="col-md-3">
        <div style="background-color:#d2469f;align-items:center;" class="text-center vcenter"><img src="Images/icon-ttkd.jpg" class="pull-left"/><label class="text-uppercase divText">TTKD VNPT Tỉnh/TP</label><div class="clearfix"></div></div>
     <div class="panel panel-default">
  <div class="panel-body"  style="min-height:280px;max-height:280px;">

       <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Bắc</div>
    <div class="DivBody">
        TTKD Hà nội<br />
        TTKD Thái Bình<br />
        TTKD Hà Nam
        
    </div>

        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Nam</div>
    <div class="DivBody">
        TTKD Hồ Chí Minh<br />
        TTKD Tiền Giang<br />
        TTKD Trà Vinh
        
    </div>
        <div class="DivTittle"><span class="glyphicon glyphicon-stop"></span> Miền Trung</div>
    <div class="DivBody">
        TTKD Huế<br />
        TTKD Quảng Bình<br />
        TTKD Nghệ An


    </div>


  </div>
                 
      <div class="pull-right" style="margin-top:-25px;"> Xem tiếp <span class="glyphicon glyphicon-forward"></span>&nbsp;</div>
</div>
    </div>
  
    
    
    </div>
</asp:content>
