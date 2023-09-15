declare module '*.jpg' {
    const jpgPath: string;
    export default jpgPath;
}
declare module '*.jpeg' {
    const jpegPath: string;
    export default jpegPath;
}
declare module '*.svg' {
    const svgPath: string;
    export default svgPath;
}
declare module "*.png" {
    const src: string
    export default src
}
declare module '*.scss' {
    const content: Record<string, string>;
    export default content;
}
declare module '*.css' {
    const content: Record<string, string>;
    export default content;
}