import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminNgosListComponent } from './admin-ngos-list/admin-ngos-list.component';
import { AdminNgosVerifyComponent } from './admin-ngos-verify/admin-ngos-verify.component';
import { AdminNGOsComponent } from './admin-ngos.component';

const routes: Routes = [
    {
        path: '', component: AdminNGOsComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },

            { path: 'list', component: AdminNgosListComponent },
            { path: 'verify/:id', component: AdminNgosVerifyComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminNgosRoutingModule {
    static routedComponents = [AdminNGOsComponent, AdminNgosListComponent, AdminNgosVerifyComponent];
}
