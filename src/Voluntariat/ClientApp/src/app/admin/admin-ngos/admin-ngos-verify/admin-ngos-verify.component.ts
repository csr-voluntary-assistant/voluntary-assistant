import { Component, OnInit } from '@angular/core';
import { NGO } from '../admin-ngos.models';
import { AdminNgosService } from '../admin-ngos.service'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'admin-ngos-verify',
  templateUrl: './admin-ngos-verify.component.html',
})

export class AdminNgosVerifyComponent implements OnInit {
  id: string;
  ngo: NGO;

  constructor(private service: AdminNgosService, private actRoute: ActivatedRoute) {  }

  ngOnInit() {
    this.actRoute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    this.service.getByID(this.id).subscribe((data: NGO) => {
      this.ngo = data;
    });
  }
}
