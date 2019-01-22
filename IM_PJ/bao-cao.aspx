<%@ Page Language="C#" Title="Báo cáo - Thống kê" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="bao-cao.aspx.cs" Inherits="IM_PJ.bao_cao" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Báo cáo - Thống kê</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product center-txt">
                                <tbody>
                                    <tr>
                                        <th>Loại báo cáo - thống kê</th>
                                        <th>Thao tác</th>
                                    </tr>
                                    <tr>
                                        <td><a href="thong-ke-doanh-thu">Doanh thu</a></td>
                                        <td><a href="thong-ke-doanh-thu" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                    <tr>
                                        <td><a href="thong-ke-loi-nhuan">Lợi nhuận</a></td>
                                        <td><a href="thong-ke-loi-nhuan" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                    <tr>
                                        <td><a href="thong-ke-so-luong-san-pham-ban-ra">Số lượng bán ra</a></td>
                                        <td><a href="thong-ke-so-luong-san-pham-ban-ra" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                    <tr>
                                        <td><a href="thong-ke-so-luong-hang-doi-tra">Số lượng đổi trả</a></td>
                                        <td><a href="thong-ke-so-luong-hang-doi-tra" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                     <tr>
                                        <td><a href="thong-ke-phi-van-chuyen">Phí vận chuyển</a></td>
                                        <td><a href="thong-ke-phi-van-chuyen" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                     <tr>
                                        <td><a href="thong-ke-chiet-khau">Chiết khấu</a></td>
                                        <td><a href="thong-ke-chiet-khau" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                     <tr>
                                        <td><a href="thong-ke-so-luong-ton-kho-theo-danh-muc">Tồn kho</a></td>
                                        <td><a href="thong-ke-so-luong-ton-kho-theo-danh-muc" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                    <tr>
                                        <td><a href="thong-ke-nhan-vien">Nhân viên</a></td>
                                        <td><a href="thong-ke-nhan-vien" class="btn primary-btn h45-btn">Chi tiết</a></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
