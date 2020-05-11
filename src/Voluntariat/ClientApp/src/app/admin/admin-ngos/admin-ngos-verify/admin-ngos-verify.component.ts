import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AdminNgosService } from '../admin-ngos.service'
import { NGO, NGOStatus } from '../admin-ngos.models';

@Component({
    selector: 'admin-ngos-verify',
    templateUrl: './admin-ngos-verify.component.html',
})

export class AdminNgosVerifyComponent implements OnInit {
    id: string;
    ngo: NGO = new NGO();
    ngoIsVerified = false;
    NGOStatus = NGOStatus;

    constructor(private service: AdminNgosService, private actRoute: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        this.actRoute.paramMap.subscribe(params => {
            this.id = params.get('id');
        });

        this.service.getByID(this.id).subscribe((data: NGO) => {
            this.ngo = data;
            this.ngoIsVerified = this.ngo.status === NGOStatus.Verified;
        });
    }

    verifyNGO(ngo) {
        this.service.verifyByID(ngo).subscribe((data: NGO) => {
            this.ngo = data;
            this.ngoIsVerified = true;
        },
            error => console.error(error)
        );
    }

    deleteNGO(id: string) {
        if (confirm("Are you sure to delete NGO?")) {
            this.service.deleteByID(id).subscribe((data: boolean) => {
                if (data) {
                    this.goToList();
                }
            },
                error => console.error(error)
            );
        }       
    }

    goToList() {
        this.router.navigate(['/admin']);
    }
}
