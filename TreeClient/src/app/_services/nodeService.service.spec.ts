/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NodeServiceService } from './nodeService.service';

describe('Service: NodeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NodeServiceService]
    });
  });

  it('should ...', inject([NodeServiceService], (service: NodeServiceService) => {
    expect(service).toBeTruthy();
  }));
});
