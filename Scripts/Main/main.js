// 자동 완성을 위한 페이지 로딩시 아이템 이름 비동기로 가져오기

$(document).ready(function () {


    var item_name = []

    setTimeout(function () {
        $.ajax({
            type: 'POST',
            url: "/Home/Get_All_Item_Name",
            dataType: 'JSON',


            success: function (data) {
                console.log(data);

                //Page Loading Item_name push to Array
                for (i = 0; i < Object.keys(data).length; i++) {
                    item_name.push(data[i].Item_name);
                }
                //onsole.log(item_name);
                //alert('아이템 이름 로딩 성공');
            },

            error: function  (_status, err) {
                console.log(err);
                //alert('아이템 이름 로딩 오류');
            }


        });
    },1000);

    //
    $("#item_name").autocomplete({
        source: function (request, response) {
            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
            response($.grep(item_name, function (item) {
                return matcher.test(item);
            }).slice(0,10));
        },

          focus: function (event, ui) {
            return false;//한글 에러 잡기용도로 사용됨
        },

        //자동완성 최소 글자수
        minLength: 2,
        autoFocus: true,
    });


});
