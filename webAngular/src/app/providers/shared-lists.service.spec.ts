import { TestBed, inject } from '@angular/core/testing';

import { SharedListsService } from './shared-lists.service';

describe('SharedListsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SharedListsService]
    });
  });

  it('should be created', inject([SharedListsService], (service: SharedListsService) => {
    expect(service).toBeTruthy();
  }));
});
