import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminCategoriesListComponent } from './admin-categories-list/admin-categories-list.component';
import { AdminCategoriesAddComponent } from './admin-categories-add/admin-categories-add.component';
import { AdminCategoriesEditComponent } from './admin-categories-edit/admin-categories-edit.component';
import { AdminCategoriesDeleteComponent } from './admin-categories-delete/admin-categories-delete.component';
import { AdminCategoriesComponent } from './admin-categories.component';

const routes: Routes = [
    {
        path: '', component: AdminCategoriesComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },

            { path: 'list', component: AdminCategoriesListComponent },
            { path: 'add', component: AdminCategoriesAddComponent },
            { path: 'edit/:id', component: AdminCategoriesEditComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminCategoriesRoutingModule {
    static routedComponents = [AdminCategoriesComponent, AdminCategoriesListComponent, AdminCategoriesAddComponent, AdminCategoriesEditComponent, AdminCategoriesDeleteComponent];
}
