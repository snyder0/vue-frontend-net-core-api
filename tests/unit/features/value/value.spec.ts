import { shallowMount } from '@vue/test-utils';
import Value from "@/features/value/Value.vue"

describe('Value.vue', () => {
  it('renders props.msg when passed', () => {
    const msg = 'Value Component';
    const wrapper = shallowMount(Value, {
      propsData: { msg },
    });
    expect(wrapper.text()).toMatch(msg);
  });
});
