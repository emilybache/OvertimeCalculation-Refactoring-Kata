
const sample = require('../src/sample.js');

describe('Sample', () => {

    it('should do something', () => {
        
        let result = sample()
        expect(result).toMatchSnapshot()
    })
});