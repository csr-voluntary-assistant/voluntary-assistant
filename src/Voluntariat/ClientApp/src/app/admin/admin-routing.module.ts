import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './admin.component';

const routes: Routes = [
    {
        path: '', component: AdminComponent,
        children: [
            { path: '', redirectTo: 'admin-ngos', pathMatch: 'full' },

            { path: 'admin-ngos', loadChildren: () => import('./admin-ngos/admin-ngos.module').then(m => m.AdminNgosModule) },
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
