import { Component, OnInit } from '@angular/core';

import { AdminNgosService } from '../admin-ngos.service'

@Component({
    selector: 'admin-ngos-list',
    templateUrl: './admin-ngos-list.component.html',
})

export class AdminNgosListComponent implements OnInit {

    displayedColumns: string[] = ['nrCrt', 'name', 'status', 'createdBy', 'verify'];

    dataSource = this.service.getAll();

    constructor(private service: AdminNgosService) { }

    ngOnInit() {
    }
}
