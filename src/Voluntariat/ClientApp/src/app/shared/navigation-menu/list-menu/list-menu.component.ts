import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { NavigationMenuModel, NavigationMenuType } from '../navigation-menu/navigation-menu.model';
import { Router } from '@angular/router';

@Component({
    selector: 'list-menu',
    templateUrl: './list-menu.component.html',
    styleUrls: ['./list-menu.component.css']
})
export class ListMenuComponent implements OnInit {

    @Input() menuList: NavigationMenuModel[] = [];
    @Input() isExpanded: boolean = false;

    @Output() isMenuItemClicked: EventEmitter<void> = new EventEmitter<void>();

    navigationMenuType = NavigationMenuType;

    constructor(private router: Router) { }

    ngOnInit() {
        this.setTextTypes();
    }

    setTextTypes() {
        this.menuList.map((m) => m.type = m.type ? m.type : this.navigationMenuType.Normal);
    }

    navigateToUrl(url: string[]) {
        this.onMenuItemClicked();

        this.router.navigate(url);
    }

    onMenuItemClicked() {
        this.isMenuItemClicked.emit();
    }
}
