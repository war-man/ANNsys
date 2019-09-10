class DiscountGroupsController {
    initPermittedUpdate() {
        // Lấy biến ẩn chứa giá trị cho phép được thêm discount group new
        let permittedDOM = document.querySelector("[id$='_hdfPermittedEdit']");
        let permitted = 0;

        if (permittedDOM)
            permitted = +permittedDOM.value || 0;

        if (!permitted)
            document.getElementById("btnAddDiscountGroup").style.display = 'none';
        else
            document.getElementById("btnAddDiscountGroup").style.display = '';
    };
};