import { Component, OnInit } from '@angular/core';
import { PoliciesService } from "../../providers/policies.service";
import { SharedListsService } from '../../providers/shared-lists.service';
import { Policy } from "../../model/policy";
import { NgForm } from '@angular/forms';
import { $ } from '../../../../node_modules/protractor';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
@Component({
  selector: 'app-policy',
  templateUrl: './policy.component.html',
  styleUrls: ['./policy.component.css']
})
export class PolicyComponent implements OnInit {

  public listClients: any;
  public policy: Policy;
  policyForm: FormGroup;
  constructor(public _policyService: PoliciesService, public _sharedListService: SharedListsService,  private fb: FormBuilder,) {
    _sharedListService.getCoverageType();
    this._policyService.getPolicies();
    this.policy = new Policy();
    this.buildForm();
  }

  ngOnInit() {
    console.log(this._sharedListService.listCoverageType);
    this.getClients();
  }

  buildForm() {
    this.policyForm = this.fb.group({
      Name: ['', Validators.compose([Validators.required]) ],
      PolicyStartDate: ['', Validators.compose([Validators.required]) ],
    });
  }

  getClients() {
    this._policyService.getClients().subscribe(
      data => {
        this.listClients = data;
      }
    );
  }

  guardar(dataForm: NgForm) {
    this._policyService.PostPolicy(this.policy).subscribe(
      (result) => {
        this._policyService.getPolicies();
        this.policy = new Policy();
        
      }
    );

  }


}


