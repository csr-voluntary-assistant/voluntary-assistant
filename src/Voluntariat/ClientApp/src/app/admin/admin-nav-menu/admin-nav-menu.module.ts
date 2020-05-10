import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminNavMenuComponent } from './admin-nav-menu/admin-nav-menu.component';
import { NavigationMenuModule } from '../../shared/navigation-menu/navigation-menu.module';



@NgModule({
  declarations: [AdminNavMenuComponent],
  imports: [
    CommonModule,
    NavigationMenuModule
  ],
  exports: [AdminNavMenuComponent]
})
export class AdminNavMenuModule { }
