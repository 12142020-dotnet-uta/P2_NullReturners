import { TestBed } from '@angular/core/testing';

import { DrawService } from './draw.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('DrawService', () => {
  let service: DrawService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(DrawService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
