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

        const host = window.location.origin;
        let temp_url = '';
        switch (url[0]) {
            case "sign-in": {
                temp_url = '/Identity/Account/Login';
                break;
            }
            case "register": {
                temp_url = '/Identity/Account/Register?registerAs=NGO';
                break;
            }
            case "ngo-tab": {
                temp_url = '/Identity/Account/Login?ReturnUrl=%2FNGOs';
                break;
            }
        }

        if (temp_url === '') {
            this.router.navigate(url);
        }
        else {
            window.location.assign(host + temp_url);
        }
    }

    onMenuItemClicked() {
        this.isMenuItemClicked.emit();
    }
}
