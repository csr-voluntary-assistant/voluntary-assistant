import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AdminNgosService } from '../admin-ngos.service'
import { NGO } from '../admin-ngos.models';

@Component({
    selector: 'admin-ngos-verify',
    templateUrl: './admin-ngos-verify.component.html',
})

export class AdminNgosVerifyComponent implements OnInit {
    id: string;
    ngo: NGO = new NGO();

    constructor(private service: AdminNgosService, private actRoute: ActivatedRoute) { }

    ngOnInit() {
        this.actRoute.paramMap.subscribe(params => {
            this.id = params.get('id');
        });

        this.service.getByID(this.id).subscribe((data: NGO) => {
            this.ngo = data;
        });
    }
}
