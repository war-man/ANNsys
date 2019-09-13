class SearchCustomerService {
    static potentialCustomerDiscount(discountGroupID, search)
    {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/danh-sach-khach-giam-gia.aspx/getPotentialCustomer",
                data: JSON.stringify({ 'discountGroupID': discountGroupID, 'search': search }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: response => {
                    let data = response.d || []
                    let customers = [];

                    data.forEach(item => {
                        let cus = new CustomerModel();
                        cus.ID = item.ID;
                        cus.FullName = item.FullName;
                        cus.Nick = item.Nick;
                        cus.Phone = item.Phone;
                        cus.Address = item.Address;
                        cus.Zalo = item.Zalo;
                        cus.Facebook = item.Facebook;
                        cus.StaffName = item.StaffName;
                        if (item.DiscountGroup)
                        {
                            let discount = new DiscountGroupModel();
                            discount.ID = item.DiscountGroup.ID,
                            discount.Name = item.DiscountGroup.Name
                            cus.DiscountGroup = discount;
                        }

                        customers.push(cus);
                    });

                    resolve(customers);
                },
                error: err => {
                    reject(err)
                }
            })
        });
    }
};