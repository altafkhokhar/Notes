import { TestBed } from '@angular/core/testing';

import { Notes.ApiService } from './notes.api.service';

describe('Notes.ApiService', () => {
  let service: Notes.ApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Notes.ApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
