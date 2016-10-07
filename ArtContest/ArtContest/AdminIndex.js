function donwloadAll() {
    var div = document.getElementById("allImages");
    console.log(div);
    var images = div.getElementsByTagName("img");
    console.log(images)   
    for (i = 0; i < images.length ; i++) {
        console.log(images[i]);
        downloadWithName(images[i].src,images[i].title);
    }
}
function downloadWithName(uri, name) {
    function eventFire(el, etype) {
        if (el.fireEvent) {
            (el.fireEvent('on' + etype));
        } else {
            var evObj = document.createEvent('MouseEvent');
            evObj.initMouseEvent(etype, true, false,
                 window, 0,
                 0, 0, 0, 0,
                 false, false, false, false,
                 0, null);
            el.dispatchEvent(evObj);
        }
    }
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    eventFire(link, "click");
}