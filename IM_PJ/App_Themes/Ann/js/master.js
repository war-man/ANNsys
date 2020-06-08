jQuery(document).ready(function($){
    
    //Show/Hide scroll-top on Scroll
    // hide #back-top first
	$("#scroll-top").hide();
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 1500) {
                $('#scroll-top').fadeIn();
                $('#scroll-bottom').fadeOut();
            } else {
                $('#scroll-bottom').fadeIn();
                $('#scroll-top').fadeOut();
            }

            if (($(this).scrollTop() > 200) && ($(this).scrollTop() < ($(document).height() - $(window).height() - 100))) {
                $('#buttonbar').show();
            }
            else {
                $('#buttonbar').hide();
            }
        });
        // scroll body to 0px on click
        $('#scroll-top').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 500);
        });
        $('#scroll-bottom').click(function () {
            $('body,html').animate({
                scrollTop: $("body").height()
            }, 500);
        });
    });
    $('.nav-toggle').on('click', function(e){
        $(this).toggleClass('open');
        $('body').toggleClass('menuin');
    });
    $('.nav-overlay').on('click', function(e){
        $('.nav-toggle').trigger('click');
    })
    $('.schedule-box.editable').on('click', function(e){
        var $this = $(this);
        $this.toggleClass('editing');
        if($this.parent('li').length){
            var $lis=  $this.parent('li');
            $lis.siblings().find('.schedule-box').removeClass('editing');
        }
    });
    $('.table-collapse').on('click', '.collapse-toggle', function(e){
        e.preventDefault();
        $(this).closest('.collapse-heading').toggleClass('in');
    });
});