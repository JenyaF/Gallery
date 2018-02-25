$('#wrapper').photobox('a',{
    time:2000,       
    thumbs:true,  
    zoomable:true 
});

$(document).ready(function () {

    //blocksit define
    $(window).load(function () {
        $('#container').BlocksIt({
            numOfCol: 4,
            offsetX: 8,
            offsetY: 8
        });
    });
    var currentWidth = 600;
    $(window).resize(function () {
        var winWidth = $(window).width();
        var conWidth;
        if (winWidth < 810) {
            conWidth = 440;
            col = 2
        } else if (winWidth < 1110) {
            conWidth = 660;
            col = 3
        } else {
            conWidth = 880;
            col = 4;
        }

        if (conWidth != currentWidth) {
            currentWidth = conWidth;
            $('#container').width(conWidth);
            $('#container').BlocksIt({
                numOfCol: col,
                offsetX: 8,
                offsetY: 8
            });
        }
    });
});
$('.demo').loupe();
    //window resize
  /*  var currentWidth = 1100;
    $(window).resize(function () {
        var winWidth = $(window).width();
        var conWidth;
        if (winWidth < 660) {
            conWidth = 440;
            col = 2
        } else if (winWidth < 880) {
            conWidth = 660;
            col = 3
        } else if (winWidth < 1100) {
            conWidth = 880;
            col = 4;
        } else {
            conWidth = 1100;
            col = 5;
        }

        if (conWidth != currentWidth) {
            currentWidth = conWidth;
            $('#container').width(conWidth);
            $('#container').BlocksIt({
                numOfCol: col,
                offsetX: 8,
                offsetY: 8
            });
        }
    });
});
*/
   