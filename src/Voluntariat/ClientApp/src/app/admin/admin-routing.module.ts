import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './admin.component';

const routes: Routes = [
    {
        path: '', component: AdminComponent,
        children: [
            { path: '', redirectTo: 'ngos', pathMatch: 'full' },
            { path: 'ngos', loadChildren: () => import('./admin-ngos/admin-ngos.module').then(m => m.AdminNgosModule) },
            { path: 'categories', loadChildren: () => import('./admin-categories/admin-categories.module').then(m => m.AdminCategoriesModule) },
            { path: 'services', loadChildren: () => import('./admin-services/admin-services.module').then(m => m.AdminServicesModule) },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule {
    static routedComponents = [AdminComponent];
}
