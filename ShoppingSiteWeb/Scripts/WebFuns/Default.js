window.onload = function () {
    var aDiv = document.getElementById('button').getElementsByTagName('div');

    var CarouselPrev = document.getElementById('CarouselPrev');
    var CarouselNext = document.getElementById('CarouselNext');

    var CarouselImg = document.getElementById('CarouselView')

    var now = 0;
    var timer2 = null;

    for (var i = 0; i < aDiv.length; i++) {
        aDiv[i].index = i;
        aDiv[i].onclick = function () {
            if (now == this.index) return;
            now = this.index;
            tab();
        }
        aDiv[i].addEventListener('mouseover', function () {
            if (this.index == now)
                this.style.background = '#004469'
            else
                this.style.background = '#FFFFFFC0'
        })
        aDiv[i].addEventListener('mouseout', function () {
            if (this.index == now)
                this.style.background = '#004469C0'
            else
                this.style.background = '#DDDDDDC0'
        })
    }

    CarouselPrev.onclick = function () {
        now--;
        if (now == -1) {
            now = aDiv.length - 1;
        }
        tab();
    }
    CarouselNext.onclick = function () {
        now++;
        if (now == aDiv.length) {
            now = 0;
        }
        tab();
    }
    CarouselImg.onmouseover = function () {
        clearInterval(timer2);
    }
    CarouselImg.onmouseout = function () {
        timer2 = setInterval(CarouselNext.onclick, 6000);
    }
    timer2 = setInterval(CarouselNext.onclick, 6000);

    function tab() {
        clearInterval(timer2);
        for (var i = 0; i < aDiv.length; i++) {
                aDiv[i].style.background = '#DDDDDDC0';
        }
        aDiv[now].style.background = '#004469C0';

        moveElement(CarouselImg, -1000 * now);
        timer2 = setInterval(CarouselNext.onclick, 6000);
    }

    function moveElement(ele, x_final) {//ele為元素物件
        var x_pos = ele.offsetLeft;

        if (ele.movement) {//防止懸停
            clearTimeout(ele.movement);
        }

        var dist = Math.ceil(Math.abs(x_final - x_pos));
        x_pos = x_pos < x_final ? x_pos + dist : x_pos - dist;

        CarouselImg.style.transform = `translateX(${x_pos}px)`;
    }
}