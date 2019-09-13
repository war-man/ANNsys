class EditDiscountGroupController {
    // Thêm Owner vào nhóm cho phép truy cập discount group
    showOwner(item) {
        this._createAccountTag(item, true);
    };
    
    // Thê hiện các accout được cho phép vô dicount group
    showAccoutPermittedAccess(accounts) {
        accounts.forEach(item => {
            this._removeAccountOption(item);
            this._createAccountTag(item);
        })
    };

    // Sự kiện handle khi thêm account cho phép nhìn thấy group triết khấu
    addAccoutPermittedAccess() {
        let accountDOM = document.querySelector("[id$='_ddlAccount']");
        let permittedReadDOM = document.querySelector("[id$='_hdfPermittedRead']");

        // Nêu không tồn tại drop downlist ddlAccount thì thoát xử lý
        if (!accountDOM || !permittedReadDOM)
            return;

        // Cập nhật accout cho phép truy cấp 
        if (+accountDOM.value || 0) {
            let permittedRead = permittedReadDOM.value || "";
            let selected = {
                'value': +accountDOM.value || 0,
                'text': accountDOM.options[accountDOM.selectedIndex].innerText || ""
            };

            // Cập nhật dữ liệu vào biến ẩn
            if (!permittedRead)
                permittedReadDOM.value = selected.value;
            else
                permittedReadDOM.value += "," + selected.value;

            // Thê hiện account cho phép dưới dạng tags css
            this._removeAccountOption(selected);
            this._createAccountTag(selected)
        };
    };

    _removeAccountOption(item) {
        let accountDOM = document.querySelector("[id$='_ddlAccount']");
        let optionDOM = document.querySelector("[id$='_ddlAccount'] > option[value='" + item.value + "']");

        if (!accountDOM)
            return;
        else
            accountDOM.value = 0;

        if (optionDOM)
            optionDOM.style.display = 'none';
    };

    _createAccountTag(item, isOwner) {
        if (isOwner === undefined)
            isOwner = false;

        let accountDOM = document.getElementById("added-account");
        let strHTML = '';

        if (!isOwner)
            strHTML += "<span id='tag-" + item.value + "' class='label label-primary'>";
        else
            strHTML += "<span id='tag-" + item.value + "' class='label label-primary owner'>";
        strHTML += "    " + item.text;
        if (!isOwner) {
            strHTML += "    <button  type='button' class='btn btn-blue' onclick='removeAccoutPermittedAccess(" + JSON.stringify(item) + ")'>";
            strHTML += "        <i class='fa fa-times'></i>";
            strHTML += "    </button>";
        }
        strHTML += "</span>";

        if (accountDOM)
            accountDOM.innerHTML += strHTML;
    };

    //  Sự kiện handle khi xóa account được phép nhìn thấy group triết khấu
    removeAccoutPermittedAccess(item) {
        this._removeAccountTag(item);
        this._createAccountOption(item);

        let permittedReadDOM = document.querySelector("[id$='_hdfPermittedRead']");

        // Nêu không tồn tại drop downlist ddlAccount thì thoát xử lý
        if (permittedReadDOM) {
            let accounts = permittedReadDOM.value || "";

            if (accounts) {
                // Chuyển "1,2,3" -> ["1", "2", "3"]
                accounts = accounts.split(',') || [];
                accounts = accounts.filter(id => { return id != item.value });
                permittedReadDOM.value = accounts.join(",");
            }
        };
    };

    _removeAccountTag(item) {
        let tagDOM = document.querySelector("#added-account > #tag-" + item.value);

        if (tagDOM)
            tagDOM.remove();
    };

    _createAccountOption(item) {
        let optionDOM = document.querySelector("[id$='_ddlAccount'] > option[value='" + item.value + "']");

        if (optionDOM)
            optionDOM.style.display = '';
    };
}