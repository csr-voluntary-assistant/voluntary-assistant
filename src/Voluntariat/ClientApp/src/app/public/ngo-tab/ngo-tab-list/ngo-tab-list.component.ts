import { Component, OnInit } from '@angular/core';
import { NGOTabService } from '../ngo-tab.service'
import { PublicNGO } from '../ngo-tab.models';

@Component({
    selector: 'app-ngo-tab-list',
    templateUrl: './ngo-tab-list.component.html'
})

export class NGOTabComponentList implements OnInit {
    displayedColumns: string[] = ['nrCrt', 'name', 'description', 'website', 'categoryName', 'serviceName'];
    dataSource: PublicNGO[] = [];
    constructor(private service: NGOTabService) { }

    ngOnInit() {
        this.service.getAll().subscribe((data: PublicNGO[]) => {
            this.dataSource = data;
        });
    }
}
