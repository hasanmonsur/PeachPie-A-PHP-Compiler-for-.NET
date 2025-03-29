<?php

namespace Library;

/** This is a sample class. */
class Class1 {

  /**
   * Our sample encoding function.
   * @param string $value String to be encoded.
   */
  function encode($value): string {
      return json_encode($value);
  }
}