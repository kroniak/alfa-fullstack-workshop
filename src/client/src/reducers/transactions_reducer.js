import * as actions from "../actions/types";

const initialState = {
  data: [],
  error: null,
  isLoading: true,
  skip: 0,
  count: 0
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case actions.TRANS_FETCH_STARTED:
      return {
        ...state,
        isLoading: state.data.length === 0 ? true : false
      };

    case actions.TRANS_FETCH_SUCCESS:
      return {
        ...state,
        data: payload.data,
        skip: payload.skip,
        error: null,
        isLoading: false,
        count: payload.data.length
      };

    case actions.TRANS_FETCH_FAILED:
      return {
        ...state,
        error: payload.error,
        skip: payload.skip,
        isLoading: false
      };

    case actions.CARDS_FETCH_FAILED:
      return initialState;

    default:
      return state;
  }
};
