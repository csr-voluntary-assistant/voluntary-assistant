import { Component, OnInit, Input } from '@angular/core';
import { NavigationMenuModel, NavigationPosition } from './navigation-menu.model';

@Component({
    selector: 'navigation-menu',
    templateUrl: './navigation-menu.component.html',
    styleUrls: ['./navigation-menu.component.css']
})
export class NavigationMenuComponent implements OnInit {

    @Input() menu: NavigationMenuModel[] = [];

    leftMenu: NavigationMenuModel[] = [];
    rightMenu: NavigationMenuModel[] = [];
    isExpanded: boolean = false;

    constructor() { }

    ngOnInit() {
        this.generateMenu();
    }

    generateMenu() {
        this.leftMenu = this.menu.filter((m) => m.position == NavigationPosition.Left).sort((a, b) => a.order - b.order);
        this.rightMenu = this.menu.filter((m) => m.position == NavigationPosition.Right).sort((a, b) => a.order - b.order);
    }

    toggleMenu() {
        this.isExpanded = !this.isExpanded;
    }

    hideMenu() {
        if (this.isExpanded) {
            this.isExpanded = false;
        }
    }
}
