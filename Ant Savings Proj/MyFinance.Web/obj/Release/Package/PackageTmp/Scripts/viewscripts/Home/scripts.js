$(document).ready(function() {
	$('.slider .flexslider').flexslider({
		animation:"slide",
		slideshow:"false",
		slideshowSpeed:7000,
		animationSpeed:600,
		easing:"swing",
		directionNav:false
	});

	$('.slider-2 .flexslider').flexslider({
		animation:"slide",
		slideshow:"false",
		slideshowSpeed:7000,
		animationSpeed:600,
		easing:"swing",
		directionNav:true,
		controlNav:false
	});

	$('.slider-2 .flexslider .flex-prev').text('');
	$('.slider-2 .flexslider .flex-next').text('');
});