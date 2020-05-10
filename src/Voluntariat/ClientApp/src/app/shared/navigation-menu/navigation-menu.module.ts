import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationMenuComponent } from './navigation-menu/navigation-menu.component';
import { ListMenuComponent } from './list-menu/list-menu.component';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  declarations: [NavigationMenuComponent, ListMenuComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatIconModule
  ],
  exports: [NavigationMenuComponent]
})
export class NavigationMenuModule { }
