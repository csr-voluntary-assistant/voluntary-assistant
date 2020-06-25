import { Component, OnInit } from '@angular/core';
import { NavigationPosition, NavigationMenuModel } from '../../shared/navigation-menu/navigation-menu/navigation-menu.model';

@Component({
  selector: 'admin-nav-menu',
  templateUrl: './admin-nav-menu.component.html',
  styleUrls: ['./admin-nav-menu.component.css']
})
export class AdminNavMenuComponent implements OnInit {

  menu: NavigationMenuModel[] = [
    {
      name: 'NGOs',
      url: ['/admin/ngos/list'],
      order: 1,
      position: NavigationPosition.Left
    },
    {
      name: 'CATEGORIES',
      url: ['/admin/categories/list'],
      order: 2,
      position: NavigationPosition.Left
    },
    {
      name: 'SERVICES',
      url: ['/admin/services/list'],
      order: 3,
      position: NavigationPosition.Left
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
