<%@ Page Title="Thêm danh mục" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="them-danh-muc.aspx.cs" Inherits="IM_PJ.them_danh_muc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm mới danh mục</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục cha
                                </div>
                                <div class="row-right">
                                   <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tên danh mục
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtCategoryName" ForeColor="Red" SetFocusOnError="true"
                                        ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Tên danh mục"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mô tả                            
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCategoryDescription" runat="server" CssClass="form-control" placeholder="Mô tả" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="row-left">               
                                </div>
                                <div class="row-right">
                                    <div class="clear">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnLogin_Click" />
                                        <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        function redirectTo(ID) {
            window.location.href = "thong-tin-danh-muc-san-pham?id=" + ID;
        }
    </script>
</asp:Content>
