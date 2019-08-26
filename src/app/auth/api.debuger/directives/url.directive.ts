import {Directive, ElementRef, OnInit} from '@angular/core';

@Directive({
    selector: '[appApiUrl]',
})
export class UrlDirective implements OnInit {
    constructor(
        private hostEl: ElementRef
    ) {}
    ngOnInit(): void {
        console.log(this.hostEl);
    }
}
