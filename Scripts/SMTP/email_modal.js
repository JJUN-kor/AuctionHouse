
$(function () {
        var currentPosition = parseInt($(".btn_up_layer").css("top"));
        $(window).scroll(function () {
        var position = $(window).scrollTop(); // 현재 스크롤바의 위치값을 반환합니다.
        $(".btn_up_layer").stop().animate({ "top": position + currentPosition + "px" }, 500);
    });
});


$(document).ready(function () {
       
})