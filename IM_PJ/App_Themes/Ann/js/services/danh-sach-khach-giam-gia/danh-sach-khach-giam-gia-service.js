class AddCustomerDiscountGroupService {
    static getOrderQualifiedOfDiscountGroup(discountGroupID, customerID)
    {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/danh-sach-khach-giam-gia.aspx/getOrderQualifiedOfDiscountGroup",
                data: JSON.stringify({ 'discountGroupID': discountGroupID, 'customerID': customerID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: response => {
                    let data = response.d || []
                    let orders = [];

                    data.forEach(item => {
                        let ord = new OrderModel();
                        let timespan = 0;

                        if (item.DateDone)
                        {
                            let regx = item.DateDone.match(/\d+/g) || [];
                            timespan = +regx[0] || 0;
                        }

                        ord.ID = item.ID;
                        ord.CustomerID = item.CustomerID;
                        ord.QuantityProduct = item.QuantityProduct;
                        ord.Price = item.Price;
                        ord.Discount = item.Discount;
                        ord.FeeShipping = item.FeeShipping;
                        ord.StaffName = item.StaffName;
                        ord.DateDone = new Date(timespan);

                        orders.push(ord);
                    });

                    resolve(orders);
                },
                error: err => {
                    reject(err)
                }
            })
        });
    }
};