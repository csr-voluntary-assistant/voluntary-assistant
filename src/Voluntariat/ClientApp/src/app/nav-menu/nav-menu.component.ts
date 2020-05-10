import { Component } from '@angular/core';
import { NavigationPosition, NavigationMenuModel, NavigationMenuType } from '../shared/navigation-menu/navigation-menu/navigation-menu.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  menu: NavigationMenuModel[] = [
    {
      name: 'NGOs',
      url: ['ngo-tab'],
      order: 1,
      position: NavigationPosition.Right
    },
    {
      name: 'VOLUNTEERS',
      url: ['ngo-tab'],
      order: 2,
      position: NavigationPosition.Right
    },
    {
      name: 'WHY US?',
      url: ['ngo-tab'],
      order: 3,
      position: NavigationPosition.Right
    },
    {
      name: 'SUBMIT NGO',
      url: ['register'],
      order: 4,
      position: NavigationPosition.Right,
      type: NavigationMenuType.Highlighted
    },
    {
      name: 'SIGN IN',
      url: ['sign-in'],
      order: 5,
      position: NavigationPosition.Right,
      type: NavigationMenuType.HighlightedWithBorder
    }
  ];

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
