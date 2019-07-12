$.ajax({
    url: '/api/CustomerApi/GetName/' + $('#customerId').text(),
    method: 'GET',
    contentType: "application/json",
    success: function (response) {
        $('span.customerName').text(response);
    }
});