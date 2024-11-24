$(function() {
	"use strict";
	initSparkline();
	skinChanger();    
    
    setTimeout(function() {
        $('.page-loader-wrapper').fadeOut();
    }, 5);
});

// Sparkline
function initSparkline() {
	$(".sparkline").each(function() {
		var $this = $(this);
		$this.sparkline('html', $this.data());
	});
}

//Skin changer
function skinChanger() {
	$('.choose-skin li').on('click', function() {
	    var $themes = $('#wrapper');
	    var $this = $(this);
  
	    var existTheme = $('.choose-skin li.active').data('theme');
	    $('.choose-skin li').removeClass('active');
	    $themes.removeClass('theme-' + existTheme);
	    $this.addClass('active');
	    $themes.addClass('theme-' + $this.data('theme'));
	});
}

// custom js 
$(document).ready(function() {

	// sidebar navigation
	$('.sidebar').metisMenu();

	// toggle fullwidth layout
	$('.btn-toggle-fullwidth').on('click', function() {
		if(!$('body').hasClass('layout-fullwidth')) {
			$('body').addClass('layout-fullwidth');
			$(this).find(".fa").toggleClass('fa-arrow-left fa-arrow-right');

		} else {
			$('body').removeClass('layout-fullwidth');
			$(this).find(".fa").toggleClass('fa-arrow-left fa-arrow-right');
		}
	});

	// off-canvas menu toggle
	$('.btn-toggle-offcanvas').on('click', function() {
		$('body').toggleClass('offcanvas-active');
	});

	$('.right_setting').on('click', function() {
		$('.setting_div').toggleClass('open');
	});

	// off-canvas menu toggle
	$('.btn-toggle-offcanvas').on('click', function() {
		$('.sidebar').toggleClass('open');
	});
	
	// RTL version
    $(".theme-rtl input").on('change',function() {
        if(this.checked) {
            $("body").addClass('rtl_mode');
        }else{
            $("body").removeClass('rtl_mode');
        }
    });

	$('#main-content').on('click', function() {
		$('body').removeClass('offcanvas-active');
	});

	$('.right_icon_btn').on('click', function() {
		$('body').toggleClass('right_icon_toggle');
	});

	// adding effect dropdown menu
	$('.dropdown').on('show.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).animate({
			top: '100%'
		}, 200);
	});

	$('.dropdown').on('hide.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).animate({
			top: '80%'
		}, 200);
	});

	// navbar search form
	$('.navbar-form.search-form input[type="text"]')
	.on('focus', function() {
		$(this).animate({
			width: '+=50px'
		}, 300);
	})
	.on('focusout', function() {
		$(this).animate({
			width: '-=50px'
		}, 300);
	});

	// Bootstrap tooltip init
	if($('[data-toggle="tooltip"]').length > 0) {
		$('[data-toggle="tooltip"]').tooltip();
	}

	if($('[data-toggle="popover"]').length > 0) {
		$('[data-toggle="popover"]').popover();
	}

	$(window).on('load', function() {
		// for shorter main content
		if($('#main-content').height() < $('#left-sidebar').height()) {
			$('#main-content').css('min-height', $('#left-sidebar').innerHeight() - $('footer').innerHeight());
		}
	});

	$(window).on('load resize', function() {
		if($(window).innerWidth() < 1280) {
			$('body').addClass('layout-fullwidth sidebar_toggle');
		} else {
			$('body').removeClass('layout-fullwidth sidebar_toggle');
		}
	});
});


// light and dark theme setting js
var toggleSwitch = document.querySelector('.theme-switch input[type="checkbox"]');
var toggleHcSwitch = document.querySelector('.theme-high-contrast input[type="checkbox"]');
var currentTheme = localStorage.getItem('theme');
if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);
  
    if (currentTheme === 'dark') {
        toggleSwitch.checked = true;
	}
	if (currentTheme === 'high-contrast') {
		toggleHcSwitch.checked = true;
		toggleSwitch.checked = false;
    }
}
function switchTheme(e) {
    if (e.target.checked) {
        document.documentElement.setAttribute('data-theme', 'dark');
		localStorage.setItem('theme', 'dark');
		$('.theme-high-contrast input[type="checkbox"]').prop("checked", false);
    }
    else {        
		document.documentElement.setAttribute('data-theme', 'light');
		localStorage.setItem('theme', 'light');
    }    
}
function switchHc(e) {
	if (e.target.checked) {
        document.documentElement.setAttribute('data-theme', 'high-contrast');
		localStorage.setItem('theme', 'high-contrast');
		$('.theme-switch input[type="checkbox"]').prop("checked", false);
    }
    else {        
		document.documentElement.setAttribute('data-theme', 'light');
		localStorage.setItem('theme', 'light');
    }  
}
toggleSwitch.addEventListener('change', switchTheme, false);
toggleHcSwitch.addEventListener('change', switchHc, false);

// toggle function
$.fn.clickToggle = function( f1, f2 ) {
	return this.each( function() {
		var clicked = false;
		$(this).bind('click', function() {
			if(clicked) {
				clicked = false;
				return f2.apply(this, arguments);
			}

			clicked = true;
			return f1.apply(this, arguments);
		});
	});
};

// Select all checkbox
$('.select-all').on('click',function(){
   
	if(this.checked){
		$(this).parents('table').find('.checkbox-tick').each(function(){
		this.checked = true;
		});
		}else{
			$(this).parents('table').find('.checkbox-tick').each(function(){
			this.checked = false;
		});
	}
});

$('.checkbox-tick').on('click',function(){
	if($(this).parents('table').find('.checkbox-tick:checked').length == $(this).parents('table').find('.checkbox-tick').length){
		$(this).parents('table').find('.select-all').prop('checked',true);
	}else{
		$(this).parents('table').find('.select-all').prop('checked',false);
	}
});

// Font Setting and icon
$(document).ready(function() {
	"use strict";
	// Font Setting 
	$('.font_setting input:radio').click(function () {
		var others = $("[name='" + this.name + "']").map(function () {
			return this.value
		}).get().join(" ")
		console.log(others)
		$('body').removeClass(others).addClass(this.value)
	});  
});

window.Iconic= {
	colors: {

		'theme-dark1': '#343a40',
		'theme-dark2': '#636d76',
		'theme-dark3': '#939697',
		'theme-dark4': '#c7c7c7',
		'theme-dark5': '#1c1818',


		'theme-cyan1': '#59c4bc',
		'theme-cyan2': '#637aae',
		'theme-cyan3': '#2faaa1',
		'theme-cyan4': '#4cc5bc',
		'theme-cyan5': '#89bab7',

		'theme-purple1': '#7954ad',
		'theme-purple2': '#e76886',
		'theme-purple3': '#782fdf',
		'theme-purple4': '#a06ee8',
		'theme-purple5': '#a390be',

		'theme-orange1': '#FFA901',
		'theme-orange2': '#600489',
		'theme-orange3': '#FF7F00',
		'theme-orange4': '#FBC200',
		'theme-orange5': '#38C172',
	},
};

// Wraptheme Website live
var Tawk_API=Tawk_API||{}, Tawk_LoadStart=new Date();
(function(){
var s1=document.createElement("script"),s0=document.getElementsByTagName("script")[0];
s1.async=true;
s1.src='https://embed.tawk.to/5c6d4867f324050cfe342c69/default';
s1.charset='UTF-8';
s1.setAttribute('crossorigin','*');
s0.parentNode.insertBefore(s1,s0);
})();