import { Component, OnInit } from '@angular/core';

import { AdminNgosService } from '../admin-ngos.service'
import { NGO } from '../admin-ngos.models';

@Component({
    selector: 'admin-ngos-list',
    templateUrl: './admin-ngos-list.component.html',
})

export class AdminNgosListComponent implements OnInit {

    displayedColumns: string[] = ['nrCrt', 'name', 'status', 'createdBy', 'verify'];
    dataSource: NGO[] = [];

    constructor(private service: AdminNgosService) { }

    ngOnInit() {
        this.service.getAll().subscribe((data: NGO[]) => {
            this.dataSource = data;
        });
    }
}
