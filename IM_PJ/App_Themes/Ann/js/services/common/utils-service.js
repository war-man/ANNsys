class UtilsService {
    static getCookie(name) {
        let v = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
        return v ? v[2] : null;
    }

    // format price
    static formatThousands(n, dp) {
        var s = '' + (Math.floor(n)),
            d = n % 1,
            i = s.length,
            r = '';
        while ((i -= 3) > 0) {
            r = ',' + s.substr(i, 3) + r;
        }
        return s.substr(0, i + 3) + r +
            (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
    };
}