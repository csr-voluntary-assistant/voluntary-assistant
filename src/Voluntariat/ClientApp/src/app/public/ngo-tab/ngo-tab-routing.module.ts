import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NGOTabComponent } from './ngo-tab.component';
import { NGOTabComponentList } from './ngo-tab-list/ngo-tab-list.component';

const routes: Routes = [
    {
        path: '', component: NGOTabComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },
            { path: 'list', component: NGOTabComponentList }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class NGOTabRoutingModule {
    static routedComponents = [NGOTabComponent, NGOTabComponentList];
}
