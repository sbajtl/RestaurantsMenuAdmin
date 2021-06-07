export class Utils {
    constructor() {
    }

    static cloneArray(from: any[], to: any[]) {
        from.forEach((v, i) => {
            const val = (typeof v === 'object') ? Object.assign({}, v) : v;
            to.push(val);
        });
    }
}
