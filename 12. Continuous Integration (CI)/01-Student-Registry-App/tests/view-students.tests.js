const assert = require('assert');
const fetch = require('node-fetch');

suite('View Students page', function() {
  test('Page title', async function() {
    let res = await fetch("http://localhost:8081/students");
    let body = await res.text();
    assert.ok(body.includes("<h1>Registered Students</h1>"));
  });
  
  test('Students list', async function() {
    let res = await fetch("http://localhost:8081/students");
    let body = await res.text();
    assert.ok(body.includes("<ul><li>Marry (marry@gmail.com)</li><li>Steve (steve@yahoo.com)</li><li>Teddy (teddy@mail.ru)</li></ul>"));
  });
});
